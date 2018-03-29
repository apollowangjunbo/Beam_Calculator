#include<stdio.h>
#include<math.h>
int main()
{
	int zhichi,//支持方式 
	    cailiao;//材料类型 
	    
	 double      moliang,//弹性模量 
	             changdu,//长度
  				anquan;//安全系数 
     char  xingzhuang,o='a',p='b',q='c',r='d',s='e';
     
       	float m[10][6]=
	{
		{
			577,3.74,100,60,352.6
		},
		{
			980,5.29,55,0
		},
		{
			372,2.14,50
		},
		{
			331.9,1.453
		},
		{
			39.2,0.199,59
		},
		{
			235,0.00669,100,0//预留给后面的补充数据 
		},
		{
			0//作用同上 
		}
	};//6代表的分别是a1；b1；人p；人0；以及需用应力；预留一个空间；
			printf("请输入截面形状代号：\n");
			printf("a  代表    矩形    截面\nb  代表    正方形   截面\nc  代表    实心圆   截面\nd  代表    空心圆   截面\n");
			scanf("%c",&xingzhuang);
	    double  A,i,h,b,D1,D2,d,y,z;//为计算截面形状相关尺寸定义变量
		//矩形截面
	if (xingzhuang==o)
	{
		printf("请输入矩形截面长h:\n");
		scanf("%lf",&h);  
		printf("\n请输入矩形截面宽b:\n");
		scanf("%lf",&b);
			if(h>b);
			else
		 {
			y=b;
			b=h;
			h=y;
		 }       
		i=sqrt(b*b/12);
		A=h*b;				//矩形截面截面相关尺寸计算完毕 
		
	
	//此处应继续添加其他截面尺寸 
	printf(" i=%f\n A=%f\n",i,A) ;
	}
	
	//正方形截面
   if(xingzhuang==p) 
   {
   	printf("请输入正方形边长a：\n");
   	scanf("%lf",&z);
   	i=sqrt(z*z/12);
   	A=z*z;
   	printf("  i=%f\n A=%f\n",i,A);
   }
   
   //实心圆截面 
   if(xingzhuang==q)
   {
   	printf("请输入实心圆的直径D：\n");
	scanf("%lf",&D1);
	i=D1/4;
	A=3.141593*D1*D1/4;
	printf("  i=%f\n A=%f\n",i,A); 
   }
   
   //空心圆截面 
	if(xingzhuang==r)
	{
		printf("请输入空心圆的外径D:\n");
		scanf("%lf",&D2);
		printf("\n请输入空心圆的内径d:\n");
		scanf("%lf",&d);
		//z=d/D2;
		i=sqrt(D2*D2+d*d)/4;
		//i=D2*y/4;
		A=3.141593*(D2*D2-d*d)/4;
		printf("  i=%f\n A=%f\n",i,A); 
	}
	else;

	
	
	//现在开始编写输入材料类型的程序段
	printf("请选择压杆材料类型：\n");
	printf("1	代表	硅钢\n2	代表	铬钼钢\n3	代表	硬铝\n4	代表	灰口铸铁\n5	代表	松木\n6	代表	结构钢\n");
	printf("\n");//还要补充其他材料
	scanf("%d",&cailiao);
	
	//现在开始编写输入长度以及弹性模量等
	printf("请输入压杆长度：（单位：m）\n");
	scanf("%lf",&changdu);
	printf("\n请输入材料的弹性模量（GPa）:\n");
	scanf("%lf",&moliang);
	printf("\n请输入压杆的稳定安全因素：\n");
	scanf("%lf",&anquan);
	
	
	//开始编写压杆的支持方式 
	printf("\n请输入压杆的支持方式：\n");
	printf("1	u=1\n");
		printf("2	u=2\n");
			printf("3	u=0.5\n");
				printf("4	u=0.7\n");
	scanf("%d",&zhichi); 
	double u;
	if(zhichi==1)
         u=1.0;
 	if(zhichi==2)
		u=2.0;
	if(zhichi==3)
		u=0.5;
	if(zhichi==4)
		u=0.7;
		
	
		
    //计算朗姆达
	double langmuda;
	langmuda=u*changdu/i;
	printf("朗姆达的值为：		%f",langmuda);//输出朗姆达的值 
	
	double  e,x,yingli;
	  x=cailiao;
	  e=langmuda;
	  if(x==1)
	  {
  		if(e<m[0][3])
  	           yingli=m[0][4];	
  	    if(e>=m[0][3]&&e<m[0][2])
  	           yingli=m[0][0]-m[0][1]*e;
  	    else
  	            yingli=3.141593*3.141593/e*moliang/e;
  	}
  	
  	if(x==2)
	  {
  		if(e<m[1][3])
  	           yingli=m[1][4];	
  	    if(e>=m[1][3]&&e<m[1][2])
  	           yingli=m[1][0]-m[1][1]*e;
  	    else
  	            yingli=3.141593*3.141593/e*moliang/e;
  	}
  	
  	if(x==3)
	  {
  		if(e<m[2][3])
  	           yingli=m[2][4];	
  	    if(e>=m[2][3]&&e<m[2][2])
  	           yingli=m[2][0]-m[2][1]*e;
  	    else
  	            yingli=3.141593*3.141593/e*moliang/e;
  	}
  	
  	if(x==4)
	  {
  		if(e<m[3][3])
  	           yingli=m[3][4];	
  	    if(e>=m[3][3]&&e<m[3][2])
  	           yingli=m[3][0]-m[3][1]*e;
  	    else
  	            yingli=3.141593*3.141593/e*moliang/e;
  	}
  	
  	if(x==5)
	  {
  		if(e<m[4][3])
  	           yingli=m[4][4];	
  	    if(e>=m[4][3]&&e<m[4][2])
  	           yingli=m[4][0]-m[4][1]*e;
  	    else
  	            yingli=3.141593*3.141593/e*moliang/e;
  	}
   	  
   	  if(x==6)
			{
				if(e<m[5][2])
				yingli=m[5][0]-m[5][1]*e*e;
				else
				yingli=3.141593*3.141593/h*moliang/h;  	  	
			}
  	  
  	    if(x==7)
			{
				if(e<m[6][2])
				yingli=m[6][0]-m[6][1]*e*e;
				else
				yingli=3.141593*3.141593/h*moliang/h;  	  	
			}
			
//考虑安全系数的结果后
         yingli=yingli/anquan; 
  	  
  printf("\n		压杆的许用应力为：\n");
  printf("		%fMPa\n",yingli);
  printf("			压杆能够承受的载荷为：\n");
  printf("		%fkN\n",yingli*A*1000);
	 
return 0;	 
}  
