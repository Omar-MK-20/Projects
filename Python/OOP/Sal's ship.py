

# Python3 program for Bubble Sort Algorithm Implementation
def bubbleSort(arr):
	n = len(arr)

	# For loop to traverse through all 
	# element in an array
	for i in range(n):
		for j in range(0, n - i - 1):
			
			# Range of the array is from 0 to n-i-1
			# Swap the elements if the element found 
			#is greater than the adjacent element
			if arr[j] > arr[j + 1]:
				arr[j], arr[j + 1] = arr[j + 1], arr[j]

def dictSort(dict1:dict):
	sorted_dict = dict(sorted(dict1.items(), key=lambda item: item[1]))
	return sorted_dict

class GroundShipping:
	# Ground Shipping, which is a small flat charge plus a rate based on the weight of your package.
	"""
	Weight of Package	                        Price per Pound	Flat Charge
	2 lb or less	                            $1.50	        $20.00
	Over 2 lb but less than or equal to 6 lb	$3.00	        $20.00
	Over 6 lb but less than or equal to 10 lb	$4.00	        $20.00
	Over 10 lb	                                $4.75	        $20.00
	"""
	name = "Ground Shipping (1)"
	weight = 0
	price = 0
	fCharge = 20
	total = 0
	
	def __init__(self, weight):
		# Initialize the weight, price, and flat charge attributes
		self.weight = weight
		if weight <= 2 :
			self.price = 1.5 * self.weight
		elif 2 < weight <= 6:
			self.price = 3 * self.weight
		elif 6 < weight <= 10:
			self.price = 4 * self.weight
		else:
			self.price = 4.75 * self.weight
		self.fCharge = 20

		self.total = self.price + self.fCharge
	
class GShippingPremium:
	# Ground Shipping Premium, which is a much higher flat charge, but you aren’t charged for weight.
	name = "Premium Ground Shipping (2)"
	price = 0
	fCharge = 125
	total = 0
	def __init__(self):
		self.total = self.fCharge
	
class DroneShipping:
	# Drone Shipping (new), which has no flat charge, but the rate based on weight is triple the rate of ground shipping.
	"""
	Weight of Package	                        Price per Pound	Flat Charge
	2 lb or less	                            $4.50	        $0.00
	Over 2 lb but less than or equal to 6 lb	$9.00	        $0.00
	Over 6 lb but less than or equal to 10 lb	$12.00	        $0.00
	Over 10 lb	                                $14.25	        $0.00
	"""
	name = "Drone Shipping (3)"
	weight = 0
	price = 0
	fCharge = 0
	total = 0

	def __init__(self, weight):
		self.weight = weight
		if weight <= 2:
			self.price = 4.5 * self.weight
		elif 2 < weight <= 6:
			self.price = 9 * self.weight
		elif 6 < weight <= 10:
			self.price = 12 * self.weight
		else:
			self.price = 14.25 * self.weight
		
		self.total = self.price + self.fCharge

print("Welcome to Sal's Shipping")
print("""
Sal’s Shippers has several different options for a customer to ship their package:
	  
	1- Ground Shipping, which is a small flat charge plus a rate based on the weight of your package.
		Weight of Package	                        Price per Pound	Flat Charge
		2 lb or less	                                $1.50	        $20.00
		Over 2 lb but less than or equal to 6 lb	$3.00	        $20.00
		Over 6 lb but less than or equal to 10 lb	$4.00	        $20.00
		Over 10 lb	                                $4.75
	
	2- Ground Shipping Premium, which is a much higher flat charge, but you aren’t charged for weight.

	3- Drone Shipping (new), which has no flat charge, but the rate based on weight is triple the rate of ground shipping.
		Weight of Package	                        Price per Pound	Flat Charge
		2 lb or less	                                $4.50	        $0.00
		Over 2 lb but less than or equal to 6 lb	$9.00	        $0.00
		Over 6 lb but less than or equal to 10 lb	$12.00	        $0.00
		Over 10 lb	                                $14.25
""")

# enter the weight
weight = float(input("Enter the weight of your package in pounds: "))

shipping1 = GroundShipping(weight)
shipping2 = GShippingPremium()
shipping3 = DroneShipping(weight)

#region of shipping details
print()
print("-1-")
print(f"Ground Shipping price: ${shipping1.price:.2f}")
print(f"Ground Shipping flat chrage: {shipping1.fCharge}")
print(f"Ground Shipping total: ${shipping1.total:.2f}")
print()

print("-2-")
print(f"Premium Ground Shipping price: ${shipping2.price:.2f}")
print(f"Premium Ground Shipping flat chrage: {shipping2.fCharge}")
print(f"Premium Ground Shipping total: ${shipping2.total:.2f}")
print()

print("-3-")
print(f"Drone Shipping price: ${shipping3.price:.2f}")
print(f"Drone Shipping flat chrage: {shipping3.fCharge}")
print(f"Drone Shipping total: ${shipping3.total:.2f}")
print()
#endregion


dict1 = {
	shipping1.name: shipping1.total,
	shipping2.name: shipping2.total,
	shipping3.name: shipping3.total
}
newDict = dictSort(dict1)
newlist = list(newDict.keys())

print(f"The lowest cheapest method of shipping a {weight} lb package")
print(f"is {newlist[0]}")
print()

# enter the methode
method = int(input("which method you would like: "))
print()

match method:
	case 1:
		print(f"The price of shippng ${shipping1.price:.2f}")
		print(f"The total cost for Ground shipping with")
		print(f"a {shipping1.weight} lb weight")
		print(f"is ${shipping1.total:.2f}.")
		print()

	case 2: 
		print(f"The flat charge of shippng is ${shipping2.fCharge:.2f}")
		print(f"The total cost for Premium ground shipping")
		print(f"is ${shipping2.total:.2f}.")
		print()

	case 3: 
		print(f"The price of shippng ${shipping3.price:.2f}")
		print(f"The total cost for Drone shipping with")
		print(f"a {shipping3.weight} lb weight")
		print(f"is ${shipping3.total:.2f}.")
		print()

	case _: print("Invalid Method! Please enter either 1, 2 or 3."); print()

