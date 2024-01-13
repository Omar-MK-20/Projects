degree = int(input("enter your Degree: "))

if degree <= 100 and degree >=50 :
    print("pass")

elif degree < 50 and degree >= 0:
    print("fail")

else:
    print("wrong degree")