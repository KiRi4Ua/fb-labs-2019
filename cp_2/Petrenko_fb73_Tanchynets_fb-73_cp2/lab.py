import csv
FILENAME = "Lab_csv.csv"

dic ={0: 'а', 1: 'б', 2: 'в', 3: 'г', 4: 'д', 5: 'е', 6: 'ж', 7: 'з',
 8: 'и', 9: 'й', 10: 'к', 11: 'л', 12: 'м', 13: 'н', 14: 'о', 15: 'п',
 16: 'р', 17: 'с', 18: 'т', 19: 'у', 20: 'ф', 21: 'х', 22: 'ц', 23: 'ч',
 24: 'ш', 25: 'щ', 26: 'ъ', 27: 'ы', 28: 'ь', 29: 'э', 30: 'ю', 31: 'я'}

r = 16
abc = [chr(ord("а") + i) for i in range(32)]
f = open('text.txt','r')
text = f.read()
f1 = open('text_plain.txt','r')
plaintext = f1.read()
keys={
	2:"ор",
	3:"гор",
	4:"миша",
	5:"карта",
	11:"возможность",
	15:"абстрагированно"

}

for key,value in keys.items():
	k=0
	plaintext1=[]
	for i in plaintext:
		plaintext1.append( chr( ((  ( (ord(i)-1072) +  (ord(value[k%len(value)])-97) )%32)+1072) ))
		k+=1
	sum = 0

	str=''.join(plaintext1)
	n = len(str)
	for i in abc:
		Nt = str.count(i)
		sum=sum+(Nt* (Nt-1) )
	        #print(Y.count(i)," ",i)
	sum = sum/(n*(n-1))
	print(key,": ",sum,"\n")

def func(r):
    sum = 0

    Y = text[0:len(text):r]
    n = len(Y)
    for i in abc:
        Nt = Y.count(i)
        sum=sum+(Nt* (Nt-1) )
        #print(Y.count(i)," ",i)
    sum = sum/(n*(n-1))
    return sum


#print(range(text))



for i in range(2,32):
    print( i," ", '{:.10f}'.format(func(i))," ",)

key=[]
for i in range(r):
	Y = text[i:len(text):r]
	print(Y)
	max_let = 0
	n=0
	for i,value in dic.items():
		if Y.count(value)>max_let:
			n=i
			max_let=Y.count(value)
	print(max_let,"",n)
	key.append(dic[(n-14)%32])
print(''.join(key))
orig_text=[]
j=0
for i in text:
	orig_text.append(chr((((ord(i)-1072)+32-(ord(key[j%16])-1072))%32)+1072))
	#print(ord(i))
	j+=1
print(''.join(orig_text))
