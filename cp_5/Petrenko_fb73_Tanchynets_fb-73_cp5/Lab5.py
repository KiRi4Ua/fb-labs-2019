import math, random

openkey = {
}
privatekey = {
"p":0,
"q":0,
"d":0
}
servkey = {
"N":0,
"E":65537
}
def Milli_rabin(num,k = 50):
    if( num==3 or num==2 ):
        return True
    if( num<2 or num%2==0):
        return False
    t = num - 1
    s=0

    while(t % 2 == 0):
        t=t//2
        s+=1

    for i in range(k):
        a = random.randint(2, num-2)
        #if (math.gcd(a,num) != 1):
        #    return False
        x = pow(a,t,num)
        if( x==1 or x==num-1):
            continue

        for j in range(s):
            #print(x)
            x1 =  pow(x,2,num)
            if ( x1==1):
                return False
            if ( x1 == num-1 ):
                break
        return False
    return True


def gen_prime(min,max):
    x = random.randint(min, max+1)
    if (x%2 == 0):
        x+=1
    for i in range((max-x)//2):
        p = x + 2*i
        if (Milli_rabin(p)):
            return p
        else:
             #return gen_prime(min,max)
             continue

def gen_prime_bit(bits):
    n0 = pow(2,bits-1)
    n1 = pow(2,bits)-1
    return gen_prime(n0,n1)

def inverse(a, m):
    a%=m;
    if a == 1:
        return 1;
    try:
        return ((1 - m * inverse(m % a, a)) // a)%m;
    except:
        return;


def generate_rsa(bits):
    q = gen_prime_bit(bits)
    while True:
        p = gen_prime_bit(bits)
        if (p!=q):
            break
    print("P=",p,'\n',"Q= ",q ,'\n')
    openkey["N"]= p*q
    print("N= ",openkey["N"])
    fi=(q-1)*(p-1)
    openkey["e"]=(pow(2,16)+1)
    print("E= ",openkey["e"])
    privatekey["d"]=inverse(openkey["e"],fi)%fi
    privatekey["q"]=q
    privatekey["p"]=p

def crypt(M,e,n):
    return pow(M,e,n)

def decrypt(C,d,n):
    return pow(C,d,n)

def Sign(M,d,n):
    return pow(M,d,n)

#print(Milli_rabin(2543766225 ,5000000))
#print(gen_prime(10,50000000))
#print(gen_prime_bit(256))
generate_rsa(256)
M=65489798486464
C=crypt(M,openkey['e'],openkey['N'])
print('\n',C)
print('\n',decrypt(C,privatekey["d"],openkey['N']))
M=97
print("M: ",M ,'\n',"Signature: ", hex(Sign(M,privatekey['d'],openkey['N'])),'\n', "Module: ",hex(openkey['N']),'\n'      )

servkey['N'] = int(input("Servmodul:") , 16)
podpis = int(input("Podpis:") , 16)
print(crypt(podpis,servkey['E'],servkey['N']))
