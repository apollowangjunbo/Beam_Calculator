#include<stdio.h>
#include<math.h>
int main()
{
	int zhichi,//֧�ַ�ʽ 
	    cailiao;//�������� 
	    
	 double      moliang,//����ģ�� 
	             changdu,//����
  				anquan;//��ȫϵ�� 
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
			235,0.00669,100,0//Ԥ��������Ĳ������� 
		},
		{
			0//����ͬ�� 
		}
	};//6����ķֱ���a1��b1����p����0���Լ�����Ӧ����Ԥ��һ���ռ䣻
			printf("�����������״���ţ�\n");
			printf("a  ����    ����    ����\nb  ����    ������   ����\nc  ����    ʵ��Բ   ����\nd  ����    ����Բ   ����\n");
			scanf("%c",&xingzhuang);
	    double  A,i,h,b,D1,D2,d,y,z;//Ϊ���������״��سߴ綨�����
		//���ν���
	if (xingzhuang==o)
	{
		printf("��������ν��泤h:\n");
		scanf("%lf",&h);  
		printf("\n��������ν����b:\n");
		scanf("%lf",&b);
			if(h>b);
			else
		 {
			y=b;
			b=h;
			h=y;
		 }       
		i=sqrt(b*b/12);
		A=h*b;				//���ν��������سߴ������� 
		
	
	//�˴�Ӧ���������������ߴ� 
	printf(" i=%f\n A=%f\n",i,A) ;
	}
	
	//�����ν���
   if(xingzhuang==p) 
   {
   	printf("�����������α߳�a��\n");
   	scanf("%lf",&z);
   	i=sqrt(z*z/12);
   	A=z*z;
   	printf("  i=%f\n A=%f\n",i,A);
   }
   
   //ʵ��Բ���� 
   if(xingzhuang==q)
   {
   	printf("������ʵ��Բ��ֱ��D��\n");
	scanf("%lf",&D1);
	i=D1/4;
	A=3.141593*D1*D1/4;
	printf("  i=%f\n A=%f\n",i,A); 
   }
   
   //����Բ���� 
	if(xingzhuang==r)
	{
		printf("���������Բ���⾶D:\n");
		scanf("%lf",&D2);
		printf("\n���������Բ���ھ�d:\n");
		scanf("%lf",&d);
		//z=d/D2;
		i=sqrt(D2*D2+d*d)/4;
		//i=D2*y/4;
		A=3.141593*(D2*D2-d*d)/4;
		printf("  i=%f\n A=%f\n",i,A); 
	}
	else;

	
	
	//���ڿ�ʼ��д����������͵ĳ����
	printf("��ѡ��ѹ�˲������ͣ�\n");
	printf("1	����	���\n2	����	�����\n3	����	Ӳ��\n4	����	�ҿ�����\n5	����	��ľ\n6	����	�ṹ��\n");
	printf("\n");//��Ҫ������������
	scanf("%d",&cailiao);
	
	//���ڿ�ʼ��д���볤���Լ�����ģ����
	printf("������ѹ�˳��ȣ�����λ��m��\n");
	scanf("%lf",&changdu);
	printf("\n��������ϵĵ���ģ����GPa��:\n");
	scanf("%lf",&moliang);
	printf("\n������ѹ�˵��ȶ���ȫ���أ�\n");
	scanf("%lf",&anquan);
	
	
	//��ʼ��дѹ�˵�֧�ַ�ʽ 
	printf("\n������ѹ�˵�֧�ַ�ʽ��\n");
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
		
	
		
    //������ķ��
	double langmuda;
	langmuda=u*changdu/i;
	printf("��ķ���ֵΪ��		%f",langmuda);//�����ķ���ֵ 
	
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
			
//���ǰ�ȫϵ���Ľ����
         yingli=yingli/anquan; 
  	  
  printf("\n		ѹ�˵�����Ӧ��Ϊ��\n");
  printf("		%fMPa\n",yingli);
  printf("			ѹ���ܹ����ܵ��غ�Ϊ��\n");
  printf("		%fkN\n",yingli*A*1000);
	 
return 0;	 
}  
