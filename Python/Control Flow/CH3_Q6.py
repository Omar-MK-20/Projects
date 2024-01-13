q=int(input("enrter a quantity: "))
p=int(input("enter a price: "))
t=q*p

if t>1000: 
    n=0.9*t
else:
    n=t

print()
print("quantity = ",q)
print("price = " ,p)
print("total = " ,t)
print("discount = ",t*0.1)
print("total net = ",n)