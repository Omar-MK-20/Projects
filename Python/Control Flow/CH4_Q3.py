import array as arr
i=0
x=0
a=arr.array('i')
while x<3:
    a.insert(i,int(input("enter a number: ")))
    if a[i]==0:
        break;
    i=i+1
total = sum(a)
avarage = total / (len(a)-1)
print()
print("total = ",total)
print("avarage = ",avarage)