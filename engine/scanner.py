import steam
import sys

steam.base.set_api_key("7A8A93D13C3E615BB293321F163A76BF")

class id_generator:
        first_id = 11101
        v = 76561197960265728

        def __init__(self):
                self.current_id = self.first_id
        
        def get_next_id(self):
                ret = self.current_id * 2 + self.v
                self.current_id = self.current_id + 1
               # print "id=" + str(ret)
                return str(ret)

        def set_start_id(self, id):
                self.current_id = id
                

def is_premium(bp):
        if bp.get_total_cells >= 300:
                return True
        return False

def has_item(bp, id):
        if id == 0: return True
        for i in bp.nextitem():
                if int(i.get_schema_id()) is int(id):
                        return True
        return False

def time_spent_under(id, time):
        stats = steam.user.user_stats(str(id))
        if stats.get_time_spent() < time or time == 0:
                return True
        return False

def search_for_item(startid, time, items):
        idgen = id_generator()
        idgen.set_start_id((int(startid)-id_generator.v)/2)

        print "items=" + str(items)
        sys.stdout.flush()
        if (len(items) == 1 and int(items[0]) == -1) or not (items is list):
                items = []
        while(True):
                try:
                        sid = idgen.get_next_id()
                        bp = steam.items.backpack(440, sid)
                        if is_premium(bp) and (not items or any( has_item(bp, id) for id in items)) and time_spent_under(sid, time):
                                print sid
                                sys.stdout.flush()
                except:
                        continue
                
# MAIN

if __name__ == "__main__":
        print "Starting profile=" + sys.argv[1] + " time=" + sys.argv[2] + " items=" + sys.argv[3]
        print "Searching..."
        sys.stdout.flush()
        search_for_item(sys.argv[1], sys.argv[2], sys.argv[3].split(','))
       # search_for_item("76561197992203636", 0, [])
