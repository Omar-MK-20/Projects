num = int(input("enter a number: "))

if num % 3 == 0:
    print("number is divisible by 3")
    
else:
    if num % 2 == 0:
        print("number is divisible by 2")
    else:
        print("the number isn't divisible by 3 nor 2")