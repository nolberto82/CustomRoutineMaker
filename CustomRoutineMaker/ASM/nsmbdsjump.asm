.nds
.create "out.bin", 0x00000000


.org	0x021095cc
bl	0x02000060


//ecode:
//.dw	 0xe2000060
//evalue:
//.dw	 0xe59400d0

.org	0x02000060


add	r1,r4,0x700

ldr	r0,[r4,0x788]
ands	r0,r0,0x20
bne	end


ldrh	r0,[r1,0xa2]

ands	r0,r0,2
movne	r0,0x3c80
strne	r0,[r4,0xd4]

end:

ldr	r0,[r4,0xd0]
bx	lr
.close
