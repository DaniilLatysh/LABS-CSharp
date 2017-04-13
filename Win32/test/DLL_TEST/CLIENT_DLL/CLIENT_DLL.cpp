// CLIENT_DLL.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <cstdio>
#include "..\DLL_TEST\DLL_TEST.h"

int main()
{
	double a = 7.4;
	int b = 99;

	cout << "a + b = " <<
		DLL_TEST::Functions::Add(a, b) << endl;
	cout << "a * b = " <<
		DLL_TEST::Functions::Multiply(a, b) << endl;
	cout << "a + (a * b) = " <<
		DLL_TEST::Functions::AddMultiply(a, b) << endl;

	return 0;
}

