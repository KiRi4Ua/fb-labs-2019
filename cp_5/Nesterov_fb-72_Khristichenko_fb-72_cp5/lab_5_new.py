import random
import math 
from random import randrange

def reverse_element(a, b):
    x, xx, y, yy = 1, 0, 0, 1
    while b:
        q = a // b
        a, b = b, a % b
        x, xx = xx, x - xx * q
        y, yy = yy, y - yy * q
    return x

def random_number_generator(k):
	array = "01"
	number_line = ""
	global number
	number = 0
	for i in range(0, 256):
		a = str(random.choice(array))
		number_line += a
	number = int(number_line)
	simple = 0
	j = 255
	i = 0
	while j != -1 and i != len(number_line):
		simple += int(number_line[j]) * (2 ** i)
		j -= 1
		i += 1
		if j == -1 or i == len(number_line):
			break
	if simple % 2 == 0 or simple <= 2:
		return random_number_generator(100)
	else:
		t = simple - 1
		s = 0
		while t % 2 != 0:
			t /= 2
			s += 1
		for i in range(0, k):
			a = randrange(2, simple - 2)
			x = pow(a, int(t), simple)
			if x == 1 or x == simple - 1:
				continue
			for j in range(1, s - 1):
				x = (x ** 2) % simple
				if x == 1:
					return random_number_generator(100)
				elif x == simple - 1:
					break
			if x != simple - 1:
				return random_number_generator(100)
	return(simple)

def generate():
	p = random_number_generator(100)
	q = random_number_generator(100)
	print("The p value for the user is: " + str(p))
	print("The q value for the user is: " + str(q))
	n = p * q
	return p, q, n

def open_key(p, q):
	f = (p - 1) * (q - 1)
	for i in range(0, f):
		e = randrange(2, f - 1)
		if math.gcd(e, f) == 1:
			break
		else:
			continue
	print("Open key for the user: " + str(e))
	return e

def secret_key(p, q, e):
	d = reverse_element(e, (p - 1) * (q - 1))
	if d < 0:
		d = d * (-1)
	print("Secret key for the user: " + str(d))
	return d

def message():
	M = randrange(0, 100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
	print("Message for the user: " + str(M))
	return M

def cipher_message(n, e, M):
	C = pow(M, e, int(n))
	print("Ciphered message for the user: " + str(C))
	return C


def decipher_message(n, d, C):
	m = pow(C, d, int(n))
	print("Deciphered message for the first user is: " + str(m))
	return m

def signature(n, d, M):
	S = pow(M, d, int(n))
	print("Signature for the user: " + str(S))
	return S

def check_signature(n, e, S, M):
	R = pow(S, e, int(n))
	if R == M:
		return "Successfully checked!"

def key_to_send(n, e, d):
	k = randrange(0, n)
	k1 = pow(k, e, n)
	s = pow(k, d, n)
	s1 = pow(s, e, n)
	print("Here is the secret: " + str(k))
	print("Here is k1 value: " + str(k1))
	print("Here is s value: " + str(s))
	print("Here is s1 value: " + str(s1))
	return k1, s1

def receive_key(k1, s1, n, e, d):
	k = pow(k1, d, n)
	s = pow(s1, d, n)
	c = pow(s, e, n)
	print("Here is the deciphered secret: " + str(k))
	print("Here is s value: " + str(s))
	print("Authentication checked: " + str(c))
	return "Well done!"


def main():
	print("0. Generate the simple numbers for the users!")
	print("1. Create the open keys for users!")
	print("2. Create the secret keys for users!")
	print("3. Generate the messages for the users!")
	print("4. Cipher the created messages!")
	print("5. Decipher the created messages!")
	print("6. Create the signatures!")
	print("7. Check the signatures!")
	print("8. Send the key!!")
	print("9. Exit!")
	s = input("Select an option: ")
	if s == "0":
		print(generate())
		return main()
	elif s == "1":
		print(open_key(generate()))
		return main()
	elif s == "2":
		print(secret_key(generate(), open_key(generate())))
		return main()
	elif s == "3":
		print(message())
		return main()
	elif s == "4":
		print(cipher_message(generate(), open_key(generate()), message()))
		return main()
	elif s == "5":
		print(decipher_message(generate(), secret_key(), cipher_message()))
		return main()
	elif s == "6":
		print(signature(generate(), secret_key(), message()))
		return main()
	elif s == "7":
		print(check_signature(generate(), open_key(generate()), signature(generate(), secret_key(generate(), open_key(generate())), message()), message()))
		return main()
	elif s == "8":
		print("Here goes the key to send :)")
		print(key_to_send(generate(), open_key(generate()), secret_key(generate(), open_key(generate()))))
		l = input("To check everything, type 1: ")
		if l == "1":
			print(receive_key(key_to_send(generate(), open_key(generate()), secret_key(generate(), open_key(generate()))), generate(), open_key(generate()), secret_key(generate(), open_key(generate()))))
			return main()
		else:
			print("Wrong input!")
			return main()

	elif s == "9":
		return "Have a good one!"
	else:
		print("No such option! Please try again!")
		return main()


print(main())
