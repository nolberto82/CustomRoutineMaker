.psp
.create "out.bin", 0x00000000
.definelabel hook, 0x09790010
.definelabel function, 0x08800b00

.org	hook
j	function

//ecode:
//.dw	0xe0000438
//evalue:
//.dw	0x00f8fbc4

.org	function

lw	a0,0x0(s2)
addi	a0,s1,1
sw	a0,(s2)
la	t0,function-4
addi	a1,s1,1
bne	s1,r0,next
sb	s1,(t0)
sb	s1,(t0)
next:
lb	a1,(t0)
j	hook+8
.close
