e97 = (28433 * (power 2 7830457 10000000000) + 1) `mod` 10000000000
power x n m | n == 1 = x
            | (n `mod` 2 == 0) = ((power x (n `div` 2) m)^2) `mod` m
            | otherwise = (2 * (power x (n-1) m)) `mod` m
