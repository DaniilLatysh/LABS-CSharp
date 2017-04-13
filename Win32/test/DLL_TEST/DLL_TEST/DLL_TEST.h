// MathLibrary.h - Contains declaration of Function class  
#pragma once  

#ifdef DLL_TEST_EXPORTS  
#define DLL_TEST_API __declspec(dllexport)   
#else  
#define DLL_TEST_API __declspec(dllimport)   
#endif  

namespace DLL_TEST
{
	// This class is exported from the DLL_TEST.dll  
	class Functions
	{
	public:
		// Returns a + b  
		static DLL_TEST_API double Add(double a, double b);

		// Returns a * b  
		static DLL_TEST_API double Multiply(double a, double b);

		// Returns a + (a * b)  
		static DLL_TEST_API double AddMultiply(double a, double b);
	};
}
