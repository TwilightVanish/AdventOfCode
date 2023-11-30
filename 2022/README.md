# 2022 Benchmarks

- Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
- Generic Single channel DDR4 2666 MHz 16GB
---
### Day 01
| Method |         Mean |      Error |     StdDev |   Gen0 | Allocated |
|------- |-------------:|-----------:|-----------:|-------:|----------:|
|  Part1 |     14.53 ns |   0.193 ns |   0.171 ns | 0.0068 |      32 B |
|  Part2 |     16.30 ns |   0.383 ns |   0.358 ns | 0.0085 |      40 B |
|  Parse | 16,023.75 ns | 273.864 ns | 256.173 ns | 0.4578 |    2232 B |
---
### Day 02
| Method |     Mean |     Error |    StdDev |   Gen0 | Allocated |
|------- |---------:|----------:|----------:|-------:|----------:|
|  Part1 | 3.887 us | 0.0443 us | 0.0393 us | 0.0381 |     200 B |
|  Part2 | 3.856 us | 0.0260 us | 0.0217 us | 0.0381 |     200 B |
---
### Day 03
| Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|------- |----------:|----------:|----------:|-------:|----------:|
|  Part1 | 15.552 us | 0.1099 us | 0.1028 us | 3.6011 |   16960 B |
|  Part2 |  9.785 us | 0.0643 us | 0.0570 us |      - |      32 B |
---
### Day 04
| Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|------- |----------:|----------:|----------:|-------:|----------:|
|  Part1 |  1.745 us | 0.0081 us | 0.0071 us | 0.0057 |      32 B |
|  Part2 |  1.661 us | 0.0077 us | 0.0072 us | 0.0057 |      32 B |
|  Parse | 63.194 us | 0.5016 us | 0.3916 us | 3.2959 |   16024 B |
---
### Day 05
| Method |     Mean |    Error |   StdDev |    Gen0 |   Gen1 | Allocated |
|------- |---------:|---------:|---------:|--------:|-------:|----------:|
|  Part1 | 15.33 us | 0.255 us | 0.238 us |  0.8545 |      - |   3.94 KB |
|  Part2 | 19.82 us | 0.388 us | 0.415 us |  4.9133 | 0.0305 |  22.62 KB |
|  Parse | 54.48 us | 0.512 us | 0.428 us | 28.5034 | 1.0376 | 130.95 KB |
---
### Day 06
| Method |     Mean |    Error |   StdDev | Allocated |
|------- |---------:|---------:|---------:|----------:|
|  Part1 | 14.19 us | 0.059 us | 0.055 us |      32 B |
|  Part2 | 52.77 us | 0.442 us | 0.413 us |      32 B |
---
### Day 07
| Method |        Mean |    Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|------- |------------:|---------:|---------:|-------:|-------:|----------:|
|  Part1 |    215.9 ns |  1.49 ns |  1.32 ns | 0.0083 |      - |      40 B |
|  Part2 |    233.8 ns |  1.76 ns |  1.56 ns | 0.0083 |      - |      40 B |
|  Parse | 11,473.3 ns | 69.11 ns | 61.27 ns | 4.4098 | 0.2594 |   20752 B |
---
### Day 08
| Method |       Mean |     Error |    StdDev |    Gen0 | Allocated |
|------- |-----------:|----------:|----------:|--------:|----------:|
|  Part1 |  15.908 us | 0.1743 us | 0.1545 us |       - |      32 B |
|  Part2 |   9.034 us | 0.0493 us | 0.0412 us |       - |      40 B |
|  Parse | 341.343 us | 1.7836 us | 1.4894 us | 16.1133 |   76856 B |
---
### Day 09 
(TODO: Optimization)

| Method |        Mean |     Error |    StdDev |    Gen0 |    Gen1 |    Gen2 | Allocated |
|------- |------------:|----------:|----------:|--------:|--------:|--------:|----------:|
|  Part1 | 1,333.37 us |  7.384 us |  6.907 us | 41.0156 | 41.0156 | 41.0156 | 315.11 KB |
|  Part2 | 5,378.58 us | 35.951 us | 31.869 us | 31.2500 |  7.8125 |       - | 150.71 KB |
|  Parse |    26.74 us |  0.320 us |  0.267 us | 14.2822 |  0.7935 |       - |  65.67 KB |
---
### Day 10
| Method |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|------- |---------:|--------:|--------:|-------:|----------:|
|  Part1 | 285.9 ns | 2.46 ns | 2.18 ns | 0.0067 |      32 B |
|  Part2 | 601.9 ns | 7.86 ns | 7.36 ns | 0.1097 |     520 B |
|  Parse | 352.7 ns | 3.29 ns | 2.92 ns | 0.2089 |     984 B |
