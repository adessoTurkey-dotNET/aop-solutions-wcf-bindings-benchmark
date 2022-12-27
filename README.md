# Benchmark Projects for 2022Q2 OKR
---

## AOP

Run using these commands;
> docker run -p 6379:6379 --name aop-redis -d redis redis-server --save 20 1 --loglevel warning --requirepass kZPhV5fS
> dotnet run -c RELEASE

### Results
|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |     False | 146,239.04 us | 5,530.29 us | 16,306.19 us | 142,320.500 us | 132,026.2000 us | 259,289.7 us |      100.0 |
|       ProfileWithProxyPattern | 34 |     False | 140,877.82 us | 5,520.59 us | 16,277.57 us | 137,663.300 us | 130,982.2000 us | 286,175.7 us |      100.0 |
|         CacheWithProxyPattern | 34 |     False |   3,247.02 us | 5,176.11 us | 15,261.87 us |   1,707.200 us |   1,279.5000 us | 154,320.2 us |      100.0 |
|           LogWithProxyPattern | 34 |     False | 140,933.69 us | 6,298.99 us | 18,572.70 us | 136,057.050 us | 131,917.3000 us | 299,852.9 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |     False | 138,728.02 us | 5,507.41 us | 16,238.72 us | 134,883.350 us | 131,095.2000 us | 280,761.9 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |     False |   2,852.98 us | 4,582.88 us | 13,512.74 us |   1,485.100 us |   1,230.4000 us | 136,620.1 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |     False | 140,049.44 us | 5,969.54 us | 17,601.34 us | 136,255.750 us | 131,423.4000 us | 298,510.1 us |      100.0 |
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|       ProfileWithProxyPattern | 34 |      True |      49.25 us |    93.22 us |    274.86 us |      20.400 us |       9.5000 us |   2,768.1 us |      100.0 |
|         CacheWithProxyPattern | 34 |      True |   3,038.50 us | 4,683.52 us | 13,809.46 us |   1,513.450 us |   1,312.7000 us | 139,623.8 us |      100.0 |
|           LogWithProxyPattern | 34 |      True |     282.03 us |   717.33 us |  2,115.05 us |      54.550 us |      23.1000 us |  21,167.1 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |      True |      29.33 us |    83.26 us |    245.50 us |       4.200 us |       2.0000 us |   2,459.1 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |      True |   2,843.39 us | 4,398.34 us | 12,968.59 us |   1,522.500 us |   1,250.5000 us | 131,218.4 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |      True |     284.93 us |   722.08 us |  2,129.08 us |      56.900 us |      25.3000 us |  21,316.8 us |      100.0 |

---

## WCF Bindings

### Results
|              Method |  n | optimized |         Mean |        Error |      StdDev |       Median |          Min |          Max | Iterations |
|-------------------- |--- |---------- |-------------:|-------------:|------------:|-------------:|-------------:|-------------:|-----------:|
|       WSHttpBinding | 34 |     False | 259,199.7 us | 13,615.19 us | 40,144.7 us | 243,625.0 us | 234,262.7 us | 456,658.3 us |      100.0 |
|       NetTcpBinding | 34 |     False | 263,610.6 us | 11,626.09 us | 34,279.8 us | 252,891.7 us | 236,485.4 us | 431,524.3 us |      100.0 |
| NetNamedPipeBinding | 34 |     False | 268,795.4 us | 11,064.98 us | 32,625.3 us | 261,013.1 us | 237,340.2 us | 414,609.1 us |      100.0 |
|       WSHttpBinding | 34 |      True |   1,950.1 us |    119.38 us |    352.0 us |   1,819.2 us |   1,618.0 us |   3,485.9 us |      100.0 |
|       NetTcpBinding | 34 |      True |     935.0 us |  1,303.51 us |  3,843.4 us |     407.4 us |     270.8 us |  38,758.2 us |      100.0 |
| NetNamedPipeBinding | 34 |      True |     604.4 us |    160.47 us |    473.2 us |     405.7 us |     240.4 us |   2,235.3 us |      100.0 |