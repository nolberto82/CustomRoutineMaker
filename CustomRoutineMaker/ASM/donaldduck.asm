.psx
.create "out.bin", 0x80000000
.definelabel hook, 0x80011828
.definelabel function, 0x80008300


.org	 hook
j	 function

//ecode:
//.dw	 0xd0000000
//evalue:
//.dh	 0x0000

.org	 function


li	t0,0xc
bne	t0,s1,end
nop
li	s1,2
end:

sb	s1,1(a0)

j	 hook+8
.close
