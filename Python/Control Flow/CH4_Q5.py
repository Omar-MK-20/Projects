import random

print("guess game")
print("the program will generate random number and you guess what the number is.")
print()

gnum = random.randint(1, 100)

# print(num)
x = int(input("guess a number from 1 to 100 : "))

while x != gnum:
    if x > gnum:
        print("the number is smaller")
    else:
        print("the number is larger")

    print()
    x = int(input("try again : "))

print()

print("your guess is right")
print("the number is: ",gnum)