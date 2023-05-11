
def check_divisibility(x, y):
   
        if x % y == 0:
            return 0
        else:
            remainder = x % y
            return remainder
    

def tower(x,y):
    if(y>=3):
        listOfOddNumbers=range(3,y-1,2)
        
        remainder=check_divisibility(x-2, len(listOfOddNumbers))
        print(" " * (y // 2 ) + '*')
        h=(x-2)//len(listOfOddNumbers)
        w=h+remainder
        for i in range(w):
           print(" " * (y // 2 - 3//2) + "*" * 3)
        for i in range(5,y-1,2):
            for j in range(h):
                   print(" " * (y // 2 - i//2) + "*" * i)
                
    print('*'*y)
         
        
    

        
def triangleScope( x,  y):
    h=y/2
    p=((x**2)+(h**2)**0.5)
    print('The scope of the triangle  is:',((p*2)+y))
     
def rectangularScope( x,  y):
    print('The scope of the rectangular is:',(x+y)*2)
def rectangularArea( x,  y):
    print('The area of the rectangular is:',(x*y))


    
n= int(input('Enter 1 for a rectangular tower\n 2 for a triangle tower\n 3 to exit')) 
while n!=3:
    leng=int(input('Enter leng'))
    width=int(input('Enter width'))
    if n==1:
         if leng == width or width>leng+5 or leng>width+5:
             rectangularArea(leng,width)
         else:
             rectangularScope(leng,width)
    else:
        w=int(input('Enter 1 for Calculate the perimeter of the triangle and 2 for print the triangle'))
        if w==1:
            triangleScope(leng,width)
        if w==2:
            if width%2==0 or width>2*leng:
                print('Sorry, the triangle cannot be printed')
            else:
                tower(leng,width)
        else:
            print('Not correct value')
    n= int(input('Enter 1 for a rectangular tower\n 2 for a triangle tower\n 3 to exit')) 
print('by')      

    
                
                    
                
    
            
            
                
                
         
                
            
        
   
   
