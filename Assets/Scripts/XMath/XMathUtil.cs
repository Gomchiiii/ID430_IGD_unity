namespace XMath {
    public static class XMathUtil {
        // Calculate the binomial coefficient C(n, k) by using Pascal's triangle
        public static int calcBinomialCoeff(int n, int k) {
            int[,] pt = new int[n + 1, n + 1]; // pt = Pascal's triangle

            // Initialize the first column with 1s
            for (int i = 0; i <= n; i++) {
                pt[i, 0] = 1;
            }

            // Calculate the binomial coefficients
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= i; j++) {
                    pt[i, j] = pt[i - 1, j - 1] + pt[i - 1, j];
                }
            }

            return pt[n, k];
        }
        public static double calcBinomialCoeff(double n, double k) {
            int nInt = (int)n;
            int kInt = (int)k;
            return (double)XMathUtil.calcBinomialCoeff(nInt, kInt);
        }
    }
}