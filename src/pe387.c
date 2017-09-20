#include <stdio.h>
#include <stdbool.h>

const unsigned long MAX=100000000000000;

unsigned long harshadsum=0;

// Should be way faster..
int isPrime(unsigned long num) {
  if (num<2) return false;
  if (num!=2 && num%2==0) return false;
  for (unsigned long i=3; i*i<=num; i+=2) {
    if (num % i ==0) return false;
  }
  return true;
}

int iterateHarshad(unsigned long h, unsigned long d, bool isstrong) {
  unsigned long int newh = h*10;
  unsigned long int s = d;

  for ( long int i=0;i<10;i++,newh++,s++) {
    if (newh >= MAX) return 0;

    if (isstrong && isPrime(newh)) {
//      printf("Harshad: %lu\n", newh);
      harshadsum+=newh;
    }

    if (newh%s==0) {
      iterateHarshad(newh,s,isPrime(newh/s));
    }
  }
  return 0;
}

int main() {
  for (unsigned long i=1;i<10;i++)
    iterateHarshad(i,i,false);

  printf("Sum: %lu\n",harshadsum);

  return 0;
}
