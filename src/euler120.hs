e120 = sum test'

test n = maximum $ zipWith (\x y -> (x+y) `mod` (n*n)) (test_m n) (test_p n)
test_m n = (n-1):[(n-1)*((test_m n)!!x) `mod` (n*n) | x<-[0..(2*n-1)]]
test_p n = (n+1):[(n+1)*((test_p n)!!x) `mod` (n*n) | x<-[0..(2*n-1)]]

test' = [(2*(2*a*a+a)) `mod` ((2*a+1)*(2*a+1)) | a<-[1..499]] ++ [(4*a*a+4*a) `mod` ((2*(a+1))*(2*(a+1))) | a<-[1..499]]
