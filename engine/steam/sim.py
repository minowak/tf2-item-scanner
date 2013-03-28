from xml.sax import saxutils
import re
import json
import base
import operator

class backpack_context(base.json_request):
    """ Reads in inventory contexts and other information
    from the root inventory page """

    def get_app(self, key):
        """ Returns context data for a given app, can be an ID or a case insensitive name """

        keystr = str(key)
        res = None

        try:
            res = self._get(keystr)
        except KeyError:
            for k, v in self._get().iteritems():
                if "name" in v and v["name"].lower() == keystr.lower():
                    res = v
                    break

        return res

    def get_app_list(self):
        """ Returns a list of valid app IDs """

        return self._get().keys()

    def get_user_id64(self):
        return self._user

    def _deserialize(self, data):
        contexts = re.search("var g_rgAppContextData = (.+);", data)

        try:
            match = contexts.group(1)
            return json.loads(match)
        except:
            raise base.items.BackpackError("No inventory information available for this user")

    def __getitem__(self, key):
        res = self.get_app(key)

        if not res: raise KeyError(key)

        return res

    def __iter__(self):
        iterindex = 0

        if not self._ctx:
            self._ctx = sorted(self._get().values(), key = operator.itemgetter("appid"))

        iterdata = self._ctx

        while iterindex < len(iterdata):
            data = iterdata[iterindex]
            iterindex += 1
            yield data

    def __init__(self, user):
        self._ctx = None
        try:
            sid = user.get_id64()
        except:
            sid = user

        url = "http://steamcommunity.com/profiles/{0}/inventory/".format(sid)
        self._user = sid

        super(backpack_context, self).__init__(url)

class backpack(base.json_request):
    def get_total_cells(self):
        """ Returns the total amount of "cells" which in this case is just an amount of items """
        return self._get("cells")

    def nextitem(self):
        iterindex = 0
        iterdata = self._get("items")

        while iterindex < len(iterdata):
            data = iterdata[iterindex]
            iterindex += 1
            yield data

    def __iter__(self):
        return self.nextitem()

    def __len__(self):
        return len(self._get("items"))

    def _get(self, value = None):
        if self._object:
            if value: return self._object[value]
            else: return self._object

        self._object = {"section": self._section, "cells": 0, "items": []}
        section = self._get("section")
        downloadlist = []
        invstr = "http://steamcommunity.com/profiles/{0}/inventory/json/{1}/"
        url = invstr.format(self._user, self._ctx["appid"])
        contexts = self._ctx["rgContexts"]
        items = []

        if section != None:
            sec = str(section)
            downloadlist.append(sec)
            self._object["cells"] = contexts[sec]["asset_count"]
        else:
            downloadlist = [str(k) for k in contexts.keys()]
            self._object["cells"] = self._ctx["asset_count"]

        downloader = base.json_request_multi()

        for sec in downloadlist:
            req = base.json_request(url + sec)
            req.section = sec
            downloader.add(req)

        requests = downloader.download()

        for page in requests:
            sec = page.section
            inventorysection = page._object

            if not inventorysection:
                raise base.items.BackpackError("Empty context data returned")

            try:
                itemdescs = inventorysection["rgDescriptions"]
            except KeyError:
                raise base.items.BackpackError("Steam returned inventory with missing context")

            inv = inventorysection.get("rgInventory")
            if not inv:
                continue

            for k, v in inv.iteritems():
                fullitem = dict(v.items() + itemdescs[v["classid"] + "_" + v["instanceid"]].items())
                finalitem = item(fullitem, contexts[sec])
                items.append(finalitem)

        self._object["items"] = items

        if value:
            return self._object[value]
        else:
            return self._object

    def __init__(self, user, app, schema = None, section = None):
        """ app: A valid app object as returned by backpack_context.get_app
        section: The inventory section to retrieve, if not given all items will be returned """

        self._object = {}
        self._section = section
        self._ctx = app

        if not app:
            raise base.items.BackpackError("No inventory available")

        try:
            sid = user.get_id64()
        except:
            sid = user

        self._user = sid

class item_attribute(base.items.item_attribute):
    def get_class(self):
        return "mult_burger"

    def get_id(self):
        # Make this be element position as well maybe
        return 0

    def get_name(self):
        return "make my name useful"

    def get_type(self):
        # Because Valve uses this same data on web pages, it's /probably/ trustworthy,
        # so long as they have fixed all the XSS bugs...
        return "html"

    def get_description(self):
        desc = self._attribute.get("value")

        if desc:
            return saxutils.unescape(desc)
        else:
            return " "

    def get_description_color(self):
        """ Returns description color as an RGB tuple """
        return self._attribute.get("color")

    def is_hidden(self):
        # Never anything but this, but could have a use for child classes
        return False

    def get_value(self):
        return 0
    def get_value_formatted(self, value = None):
        return str(self.get_value())
    def get_value_max(self):
        return 0
    def get_value_min(self):
        return 0
    def get_value_type(self):
        return self._attribute.get("type", "additive")

    def __init__(self, attribute):
        super(item_attribute, self).__init__(attribute)

class item(base.items.item):
    def get_category_name(self):
        """ Returns the category name that the item is a member of """
        return self._ctx.get("name", self._ctx["id"])

    def get_color(self):
        """ Returns the color associated with the item as a hex RGB tuple """
        return self._item.get("background_color")

    def get_quality(self):
        for tag in self._get_category("Quality"):
            # Could maybe unpack hex values into ad-hoc ID.
            return {"id": 0, "prettystr": tag["name"], "str": tag["internal_name"]}

        return {"id": 0, "prettystr": "Normal", "str": "normal"}

    def get_name(self):
        return saxutils.unescape(self._item["name"])

    def get_name_color(self):
        """ Returns the name color as an RGB tuple """
        return self._item.get("name_color")

    def get_full_item_name(self, prefixes = {}):
        return self.get_name()

    def is_untradable(self):
        return bool(not self._item.get("tradable"))

    def get_quantity(self):
        return int(self._item["amount"])

    def get_attributes(self):
        # Use descriptions here, with alternative attribute class
        return [item_attribute(attr) for attr in self._item.get("descriptions", [])]

    def get_position(self):
        return self._item["pos"]

    def get_equipped_classes(self):
        # Unsupported
        return []

    def get_equipable_classes(self):
        # might as well be unsupported
        return []

    def get_schema_id(self):
        # Kind of unsupported (class ID possible?) TODO
        return self._item["classid"]

    def get_type(self):
        return self._item.get("type", '')

    def get_image(self, size):
        """ If not one of the standard ITEM_* constants,
        the given string will be used in the request.

        Syntax is W[f]xH[f] where f is a flag scaling up to the
        given dimension until at the max size, and padding the rest with alpha/transparency """
        smallicon = self._item.get("icon_url")

        if not smallicon:
            return ""

        fullurl = self._cdn_url + smallicon
        dims = size

        if size == self.ITEM_IMAGE_SMALL: dims = "96fx96f"
        elif size == self.ITEM_IMAGE_LARGE: dims = "512fx512f"

        return fullurl + '/' + dims

    def get_id(self):
        return long(self._item["id"])

    def get_level(self):
        return None

    def get_slot(self):
        # (present sometimes in the form of tags) TODO
        for tag in self._get_category("Type"):
            return tag["name"]

    def get_description(self):
        # (kind of iffy here, since the actual descriptions are lists used for attributes)
        return None

    def is_name_prefixed(self):
        # Always false because of the nature of this inventory system, there's no accurate way to determine grammar
        return False

    def get_kill_eaters(self):
        # If they're there we can't tell due to lessened granularity, and Valve specific
        return []

    def get_rank(self):
        # see above
        return None

    def get_styles(self):
        # get_styles (Valve specific, and also iffy because it's 
        # technically there, but would need a heuristic because it's 
        # in the other description elements), the same is true for other related methods
        return []

    # get_capabilities (Maybe, depending on potential heuristic and tags) TODO

    def _get_category(self, name):
        cats = []

        if "tags" in self._item:
            for tag in self._item["tags"]:
                if tag["category"] == name:
                    cats.append(tag)

        return cats

    def __init__(self, theitem, context):
        self._cdn_url = "http://cdn.steamcommunity.com/economy/image/"
        self._ctx = context

        super(item, self).__init__(theitem)

class item_schema(base.json_request):
    def __getitem__(self, key):
        raise KeyError(key)

    def __iter__(self):
        while False: yield None

    def _get(self, value = None):
        return None

    def get_attributes(self):
        return []

    def get_particle_systems(self):
        return {}

    def get_qualities(self):
        return {}

    def __init__(self, lang = None, **kwargs):
        self._app_id = 0
        self._language = base.get_language(lang)[0]
        self._item_type = item
