.psp
.create "out.bin", 0x00000000
.definelabel hook, 0x09790008
.definelabel function, 0x08800b00

.org	hook
j	function

//ecode:
//.dw	0xe0000438
//evalue:
//.dw	0x00f8fbc4

.org	function

sw	a0,0x20(sp)


la	t0,0x099a53e0
addi	t1,t0,0x70
li	t3,4
next:
li	t2,7
move	t5,t1
loop:
sw	t2,(t0)
subi	t2,1
sw	t2,4(t0)
lw	t4,8(t5)
sw	t4,8(t0)
lw	t4,12(t5)
sw	t4,12(t0)
subi	t6,1
subi	t0,0x10
subi 	t5,0x10
bne	t2,r0,loop
nop
subi	t3,1
bne	t3,r0,next
nop
end:
j	hook+8
.close
