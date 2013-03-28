import steam

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
        for i in bp.nextitem():
                if int(i.get_schema_id()) is int(id):
                        return True
        return False

def time_spent_under(id, time):
        stats = steam.user.user_stats(str(id))
        if stats.get_time_spent() < time:
                return True
        return False

# MAIN

idgen = id_generator()

while(True):
        try:
                sid = idgen.get_next_id()
                bp = steam.items.backpack(440, sid)

                if is_premium(bp):
                        #print 'User is premium'
                        if has_item(bp, 143):
                                print 'Found earbuds'
                                file = open("premium_users.txt", "a")
                                file.write(sid + "\n")
                        stats = steam.user.user_stats("76561198023989090")
                        #print "Played for: " + str(stats.get_time_spent())
                else:
                        print 'User is F2P'
        except:
                continue
