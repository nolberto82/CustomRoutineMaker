.nds
.create "out.bin", 0x00000000


.org	0x020e31a4
bl	0x020000c0


//ecode:
//.dw	 0xe20000c0
//evalue:
//.dw	 0xe1a06000

.org	0x020000c0


ands	r6,r2,4
orrne	r2,2

mov	r6,r0



bx	lr
.close
