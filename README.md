# Benchmark Projects for 2022Q2 OKR
---

## AOP

### Results
|                        Method |  _city |     Mean |     Error |   StdDev |      Min |      Max |   Median | Iterations |
|------------------------------ |------- |---------:|----------:|---------:|---------:|---------:|---------:|-----------:|
|                       Default | Ankara | 226.0 ms |  44.50 ms | 11.56 ms | 217.6 ms | 244.0 ms | 219.5 ms |      5.000 |
|       ProfileWithProxyPattern | Ankara | 232.1 ms |  28.22 ms |  7.33 ms | 222.7 ms | 243.2 ms | 231.3 ms |      5.000 |
|         CacheWithProxyPattern | Ankara |       NA |        NA |       NA |       NA |       NA |       NA |         NA |
|           LogWithProxyPattern | Ankara | 241.3 ms |  32.62 ms |  8.47 ms | 233.0 ms | 253.4 ms | 238.7 ms |      5.000 |
| ProfileWithCastleDynamicProxy | Ankara | 239.5 ms |  37.61 ms |  9.77 ms | 230.7 ms | 253.0 ms | 235.9 ms |      5.000 |
|   CacheWithCastleDynamicProxy | Ankara | 233.0 ms |  68.35 ms | 17.75 ms | 216.9 ms | 259.8 ms | 224.8 ms |      5.000 |
|     LogWithCastleDynamicProxy | Ankara | 233.3 ms |  43.92 ms | 11.41 ms | 214.4 ms | 243.1 ms | 236.1 ms |      5.000 |
|                       Default |  Bursa | 236.1 ms |  58.75 ms | 15.26 ms | 215.8 ms | 255.8 ms | 232.3 ms |      5.000 |
|       ProfileWithProxyPattern |  Bursa | 233.1 ms |  45.21 ms | 11.74 ms | 221.0 ms | 245.4 ms | 233.7 ms |      5.000 |
|         CacheWithProxyPattern |  Bursa |       NA |        NA |       NA |       NA |       NA |       NA |         NA |
|           LogWithProxyPattern |  Bursa | 237.6 ms |  55.70 ms | 14.46 ms | 223.5 ms | 260.3 ms | 233.3 ms |      5.000 |
| ProfileWithCastleDynamicProxy |  Bursa |       NA |        NA |       NA |       NA |       NA |       NA |         NA |
|   CacheWithCastleDynamicProxy |  Bursa | 233.9 ms |  67.75 ms | 17.59 ms | 215.6 ms | 257.2 ms | 230.4 ms |      5.000 |
|     LogWithCastleDynamicProxy |  Bursa | 224.8 ms |  58.38 ms | 15.16 ms | 205.7 ms | 243.0 ms | 222.0 ms |      5.000 |
|                       Default |  Izmir | 252.3 ms | 117.47 ms | 30.51 ms | 224.8 ms | 304.3 ms | 246.7 ms |      5.000 |
|       ProfileWithProxyPattern |  Izmir | 241.3 ms |  59.82 ms | 15.54 ms | 219.8 ms | 257.9 ms | 243.1 ms |      5.000 |
|         CacheWithProxyPattern |  Izmir |       NA |        NA |       NA |       NA |       NA |       NA |         NA |
|           LogWithProxyPattern |  Izmir | 235.5 ms |  36.28 ms |  9.42 ms | 222.2 ms | 245.1 ms | 233.2 ms |      5.000 |
| ProfileWithCastleDynamicProxy |  Izmir | 243.0 ms |  32.89 ms |  8.54 ms | 236.9 ms | 257.5 ms | 239.1 ms |      5.000 |
|   CacheWithCastleDynamicProxy |  Izmir |       NA |        NA |       NA |       NA |       NA |       NA |         NA |
|     LogWithCastleDynamicProxy |  Izmir | 224.8 ms |  52.91 ms | 13.74 ms | 209.5 ms | 237.4 ms | 233.0 ms |      5.000 |

---

## WCF Bindings

### Results
|              Method |  n | optimized |         Mean |        Error |      StdDev |       Median |          Min |          Max | Iterations |
|-------------------- |--- |---------- |-------------:|-------------:|------------:|-------------:|-------------:|-------------:|-----------:|
|       WSHttpBinding |  6 |     False |   1,945.4 us |    142.12 us |    419.0 us |   1,859.5 us |   1,490.9 us |   4,670.9 us |      100.0 |
|       NetTcpBinding |  6 |     False |   1,589.4 us |    902.95 us |  2,662.4 us |     638.8 us |     305.5 us |  23,724.5 us |      100.0 |
| NetNamedPipeBinding |  6 |     False |     556.0 us |    136.35 us |    402.0 us |     411.9 us |     213.8 us |   2,225.1 us |      100.0 |
|       WSHttpBinding |  6 |      True |   1,660.7 us |     94.35 us |    278.2 us |   1,564.3 us |   1,436.6 us |   3,225.5 us |      100.0 |
|       NetTcpBinding |  6 |      True |     319.3 us |     69.03 us |    203.6 us |     249.7 us |     243.7 us |   1,448.4 us |      100.0 |
| NetNamedPipeBinding |  6 |      True |     357.7 us |    100.76 us |    297.1 us |     236.2 us |     208.9 us |   1,702.5 us |      100.0 |
|       WSHttpBinding | 16 |     False |   1,896.8 us |    226.82 us |    668.8 us |   1,746.0 us |   1,509.6 us |   7,726.9 us |      100.0 |
|       NetTcpBinding | 16 |     False |   2,089.2 us |  1,074.21 us |  3,167.3 us |     568.2 us |     290.2 us |  20,711.4 us |      100.0 |
| NetNamedPipeBinding | 16 |     False |     601.5 us |    283.48 us |    835.9 us |     304.1 us |     261.1 us |   5,282.3 us |      100.0 |
|       WSHttpBinding | 16 |      True |   1,886.3 us |    124.85 us |    368.1 us |   1,753.8 us |   1,452.3 us |   3,395.6 us |      100.0 |
|       NetTcpBinding | 16 |      True |     659.9 us |    369.40 us |  1,089.2 us |     352.4 us |     236.9 us |   9,986.9 us |      100.0 |
| NetNamedPipeBinding | 16 |      True |   1,935.4 us |  1,426.48 us |  4,206.0 us |     347.2 us |     213.9 us |  19,990.3 us |      100.0 |
|       WSHttpBinding | 34 |     False | 259,199.7 us | 13,615.19 us | 40,144.7 us | 243,625.0 us | 234,262.7 us | 456,658.3 us |      100.0 |
|       NetTcpBinding | 34 |     False | 263,610.6 us | 11,626.09 us | 34,279.8 us | 252,891.7 us | 236,485.4 us | 431,524.3 us |      100.0 |
| NetNamedPipeBinding | 34 |     False | 268,795.4 us | 11,064.98 us | 32,625.3 us | 261,013.1 us | 237,340.2 us | 414,609.1 us |      100.0 |
|       WSHttpBinding | 34 |      True |   1,950.1 us |    119.38 us |    352.0 us |   1,819.2 us |   1,618.0 us |   3,485.9 us |      100.0 |
|       NetTcpBinding | 34 |      True |     935.0 us |  1,303.51 us |  3,843.4 us |     407.4 us |     270.8 us |  38,758.2 us |      100.0 |
| NetNamedPipeBinding | 34 |      True |     604.4 us |    160.47 us |    473.2 us |     405.7 us |     240.4 us |   2,235.3 us |      100.0 |