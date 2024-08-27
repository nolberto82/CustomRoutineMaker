.psp
.create "out.bin", 0x00000000


.org	 0x08950cf4
j	 0x08801000

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x08801000


//
lwc1	f0,-0x1ea0(v0)

lui	t0,0x4000
mtc1	t0,f0
j	 0x08950cf4+8
.close
