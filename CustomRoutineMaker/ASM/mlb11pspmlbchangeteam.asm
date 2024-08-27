.psp
.create "out.bin", 0x00000000


.org	 0x09821250
j	 0x08801580

//ecode:
//.dw	 0xe000014d
//evalue:
//.dw	 0x01021250

.org	 0x08801580


lui	t0,0x999
lw	t0,0x7944(t0)
lw	t0,8(t0)
bne	t0,0,end2
nop
la	t3,0x08801570
lbu	t6,(t3)
li	t5,0x3f
bne	t6,zero,dec
lbu	t0,0x08a0ff00+0x67
bne	t5,t0,right
li	t2,0x1e
addi	v0,v0,1
bgt	t2,v0,res
nop
li	v0,0x0
b	res

right:
li	t5,0xbf
bne	t5,t0,end
nop
addi	v0,v0,-1
bgez	v0,res
nop
li	v0,0x1d
res:
li	t6,0x21
dec:
addi	t6,t6,-1
sb	t6,(t3)
end:
sb	v0,0x43(a0)
sb	v0,0x45(a0)
sb	v0,0x46(a0)
end2:
j	0x09821788

.close
