.3ds
.create "out.bin", 0x00000000


.org	0x002cab48
bl	0x00569500


//ecode:
//.dw	 0xe0569500
//evalue:
//.dw	 0x00000000

.org	0x00569500


push	{r1}

ldrb	r1,[r0,0x10]
cmp	r1,0x46
movne	r0,1
ldreqb  r0,[r0,#0x51]
pop	{r1}

bx	lr
.close
