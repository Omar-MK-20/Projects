import sys

print("Cofe Shop")
print(" 1.tea--5p \n 2.coffee--10p \n 3.milk tea--15p \n 4.Roselle--7p \n 5.mint--5p")

inum = int(input("pls, choose an item number: "))
item = "not found"

if inum == 1:
    item = "tea"
    price = 5

elif inum == 2:
    item = "coffee"
    price = 3
    
elif inum == 3:
    item = "milk tea"
    price = 15
    
elif inum == 4:
    item = "Roselle"
    price = 7

elif inum == 5:
    item = "mint"
    price = 5
    
else:
    print("wrong item")
    sys.exit()

q = int(input("pls, enter the quantity: "))

print()
print("item: ",item)
print("price: ",price)
print("quantity: ",q)
print("total: ",price*q)