"""
Module for reading from the remote storage service

Copyright (c) 2010, Anthony Garcia <lagg@lavabit.com>

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted, provided that the above
copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
"""

import json, base

class UGCError(base.APIError):
    def __init__(self, msg):
        self.msg = msg
        base.APIError.__init__(self, msg)

class user_ugc(base.json_request):
    def get_file_size(self):
        """ Size in bytes """

        return self._get("size")

    def get_local_filename(self):
        """ Local filename is what the user named it, not the URL """

        return self._get("filename")

    def get_url(self):
        """ UGC link """

        return self._get("url")

    def _deserialize(self, data):
        res = super(user_ugc, self)._deserialize(data)

        if "status" in res:
            if res["status"]["code"] == 9:
                raise UGCError("Code 9")

        return res["data"]

    def _get(self, value = None):
        try:
            return super(user_ugc, self)._get(value)
        except base.APIError as E:
            raise UGCError(str(E))

    def __init__(self, appid, ugcid64, user = None):
        uid = None

        if user:
            try: uid = user.get_id64()
            except AttributeError: uid = user

        url = ("http://api.steampowered.com/ISteamRemoteStorage/" +
               "GetUGCFileDetails/v1?ugcid={0}&appid={1}&key={2}").format(ugcid64, appid, base.get_api_key())

        if uid: url += "&steamid=" + str(uid)

        super(user_ugc, self).__init__(url)
