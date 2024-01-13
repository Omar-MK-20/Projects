num = list()
for o in range (0,10):
    num.append(int(input("enter the number: ")))
    
print()

for i in range(0,10):
    print(num[i])

    if num[i] > 0:
        print("this numbers is positive")
    elif num[i] < 0:
        print("this numbers is negative")
    else:
        print("the number is zero")