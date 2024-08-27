.psp
.create "out.bin", 0x00000000


.org	 0x0978fff4
j	 main

//ecode:
//.dw	 0xe0040438
//evalue:
//.dw	 0x00f8fbc4

.org	 0x08800f00

main:

li	t0,0x099a5230
li	t1,0x099a5310
li	t3,5
jal	copy
addi	t1,t1,0x20
li	t3,5
jal	copy

li	s7,-0xf9
j	0x0978fff4+8

copy:
lw	t2,0(t4)
sw 	t2,0(t1)

addi 	t4,t1,4
addi 	t1,t1,4
addi	t3,t3,-1
bne	t3,0,copy
nop
jr	ra

.close

