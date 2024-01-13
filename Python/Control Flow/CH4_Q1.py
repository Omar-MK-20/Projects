numi = 0
numo = 0
nume = 0

for i in range(1,1001,1):
    numi = numi + i

    if i %2 == 0:
        nume += i
    else:
        numo += i

print("the sum of int numbers = ",numi)    
print("the sum of odd numbers = ",numo)
print("the sum of even numbers = ",nume)
print()
print("the sum of all = ",numi+numo+nume)