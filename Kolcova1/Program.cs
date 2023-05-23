double[] dTarr = new double[] { 0.1, 0.001 };
double h = 0.1;
double x_0 = 0;
double x_n = 1;
double t_0 = 0;
double t_n = 1;


int x_nums = (int)((x_n - x_0) / h);


foreach (double dT in dTarr)
{
    int N = (int)((t_n - t_0) / dT);
    double[,] U = new double[N + 1, x_nums + 1];


    for (int x_index = 0; x_index <= x_nums; x_index++)
        U[0, x_index] = functionTZero(GetValueByIndex(x_0, h, x_index));


    int n = 0;
    while (n < N)
    {
        for (int x_index = 1; x_index < x_nums; x_index++)
            U[n + 1, x_index] = functionOthers(U, dT, h, n, x_index);


        U[n + 1, 0] = 0;
        U[n + 1, x_nums] = (n+1)*dT + 1;


        n++;
    }
    printU(U, x_0, h, dT, t_0);
}


static double GetValueByIndex(double start, double h, int index) => start + Convert.ToDouble(index) * h;


static double functionTZero(double x) => x + 1;


static double functionOthers(double[,] U, double dT, double h, int n, int j)
    => U[n, j] +
    dT / (h * h) * (U[n, j + 1] - 2 * U[n, j] + U[n, j - 1]) +
    dT * (j-1)*h;




static void printU(double[,] U, double x_start, double h, double dT, double t_start)
{
    Console.WriteLine($"dT = {dT}; h = {h}");
    Console.Write(String.Format("|{0, 6}|", "\\"));
    for (int i = 0; i < U.GetLength(1); i++)
        if (dT != 0.001)
            Console.Write(String.Format("{0, 18: 0.00}|", GetValueByIndex(x_start, h, i)));
        else
            Console.Write(String.Format("{0, 8: 0.000}|", GetValueByIndex(x_start, h, i)));


    Console.WriteLine();
    for (int i = 0; i < U.GetLength(0); i++)
    {
        Console.Write(String.Format("|{0, 5: 0.000}|", GetValueByIndex(t_start, dT, i)));
        for (int j = 0; j < U.GetLength(1); j++)
            if (dT != 0.001)
                Console.Write(String.Format("{0, 18: 0.00}|", U[i, j]));
            else
                Console.Write(String.Format("{0, 8: 0.000}|", U[i, j]));
        Console.WriteLine();
    }
}
