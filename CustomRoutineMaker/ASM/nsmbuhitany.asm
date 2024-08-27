.nds
.create "out.bin", 0x00000000


.org	0x00159954
bl	0x009aea80


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x009aea80


push	{r1-r2}
ldr	r1,=0xe92d48f0
ldr	r2,=0x201
ands	r6,r2
bne	starcoin

ands 	r8,0x120
bne	starcoin

cmp	r7,3
beq	starcoin

movgt	r0,1

starcoin:
ldr	r2,[sp,0x14]
cmp	r2,0
beq	end

ldr	r2,[r2]
cmp	r2,r1
bne	end

cmp	r8,0x20
moveq	r0,1

end:
pop	{r1-r2}
cmp	r0,1
bx	lr
.pool
.close