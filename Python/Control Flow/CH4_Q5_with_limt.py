import random

print("guess game")
print("the program will generate random number and you guess what the number is.")
print("you have 7 tries")
print()

gnum = random.randint(1, 100)

# print(num)
x = int(input("guess a number from 1 to 100 : "))
i = 6

while x != gnum and i > 0:
    if x > gnum:
        print("the number is smaller")
    else:
        print("the number is larger")

    print("tries left: ",i)
    i = i - 1

    print()
    x = int(input("try again : "))

print()

if x == gnum:
    print("your guess is right")
    print("the number is: ",gnum)
else:
    print("you failed")
    print("the number is: ", gnum)