import array as arr

a=arr.array('i',[1,3,5,7,9])
print("Orginial array:",a)

print("Remove the third item from the array:")
del a[2]
print("New array:",a)