from collections import deque
from collections import Counter



p1 = (1,1,0,0,0,0,1,1,0,0,1,0,1,0,1,0,1,0,0,0)
p2 = (1,1,1,0,0,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,1,0,0)

result = []
result1 = []

def import_data(filename):
	with open(filename, 'r', encoding='utf-8') as f:
		return f.read()

def lrp(polynom,res):

	imp_func = deque()
	register = deque()
	period = 0


	for i in range(len(polynom) - 1):
		imp_func.append(0)
	imp_func.append(1)

	register = imp_func.copy()

	while True:

		temp = register[0] * polynom[0]

		for i in range(1, len(register)):
			temp = (temp + (register[i] * polynom[i]))%2


		a = register.popleft()
		
		res.append(a)
		period = period + 1

		register.append(temp)

		if register == imp_func:

			return period

def ngrams(text, leng):


		freq = {}

		for i in range( len(text) - leng + 1):
			if text[i : i + leng] in freq:
				freq[text[i : i + leng]] += 1
			else:
				freq[text[i : i + leng]] = 1

		return freq

def autocor(text, per,d):

	sum = 0

	for i in range(period):

		sum += (int(text[i]) + int(text[(i + d) % period])) % 2

	return sum

period = lrp(p1,result)

print (period)

print (ngrams(''.join(map(str, result)),1))
print (ngrams(''.join(map(str, result)),2))
print (ngrams(''.join(map(str, result)),3))
print (ngrams(''.join(map(str, result)),4))
print (ngrams(''.join(map(str, result)),5))

print (autocor(''.join(map(str, result)),period,0))
print (autocor(''.join(map(str, result)),period,1))
print (autocor(''.join(map(str, result)),period,2))
print (autocor(''.join(map(str, result)),period,3))
print (autocor(''.join(map(str, result)),period,5))
print (autocor(''.join(map(str, result)),period,5))
print (autocor(''.join(map(str, result)),period,6))
print (autocor(''.join(map(str, result)),period,7))
print (autocor(''.join(map(str, result)),period,8))
print (autocor(''.join(map(str, result)),period,9))
print (autocor(''.join(map(str, result)),period,10),'\n')


period1 = lrp(p2,result1)

print (period1)

print (ngrams(''.join(map(str, result1)),1))
print (ngrams(''.join(map(str, result1)),2))
print (ngrams(''.join(map(str, result1)),3))
print (ngrams(''.join(map(str, result1)),4))
print (ngrams(''.join(map(str, result1)),5))

print (autocor(''.join(map(str, result1)),period1,0))
print (autocor(''.join(map(str, result1)),period1,1))
print (autocor(''.join(map(str, result1)),period1,2))
print (autocor(''.join(map(str, result1)),period1,3))
print (autocor(''.join(map(str, result1)),period1,5))
print (autocor(''.join(map(str, result1)),period1,5))
print (autocor(''.join(map(str, result1)),period1,6))
print (autocor(''.join(map(str, result1)),period1,7))
print (autocor(''.join(map(str, result1)),period1,8))
print (autocor(''.join(map(str, result1)),period1,9))
print (autocor(''.join(map(str, result1)),period1,10))