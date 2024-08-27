.gba
.create "out.bin", 0x0

.definelabel addr,0x0800c778
.definelabel jump,0x0203ff00

.org	addr
ldr	r0,=jump+1
bx	r0
.pool


.org	0x0203ff00


ldr	r1,=addr+9
push	r1
push	{r6-r7}

ldr	r6,=0x02000906
ldrh	r6,[r6]
ldr	r7,=0x8000
and	r6,r7
beq	end

ldr	r1,=0xfffa0000
str	r1,[r3,0x20]
	
end:

add	r0,r3
add	r0,0x23
ldrb	r0,[r0]
lsl	r0,r0,0x18
pop	{r6-r7}
pop	pc
.pool
.close
