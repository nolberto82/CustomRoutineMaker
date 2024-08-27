.psp
.create "out.bin", 0x00000000


.org	 0x08800000
j	 0x08801000

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x08801000


lui 	t0,0x3ea8


mtc1	t0,f1


j	 0x08801008
.close
