def binary_search(list, item):
    low = 0
    high = len(list)-1

    while low <= high:
        mid = int((low + high) / 2)
        guess = list[mid]
        #print(mid)
        if guess == item:
            return mid,guess
        if guess > item:
            high = mid - 1
        else:
            low = mid + 1
    return None

#my_list = ['ahmed', 'badr' ,'cilia' ,'ehab','dany' ,'fatima' ,'gamal','hany','ismaeel','janna','kamal','lourance','mohammad','nada','omar','paher','qamar','rahma','samy','tamer','usama','vardy','wassab','xamerin','yousof','zahraa']
#print(binary_search(my_list,'fatima'))


def simple_search(list,item):
    for x in range(0,(len(list))):
        #print(x)
        if list[x] == item:
            return x,list[x]

    return None

