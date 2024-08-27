.gba
.create "out.bin", 0x00000000
.definelabel branch,0x0802813c
.definelabel function,0x0203ff00


.org	branch
ldr	r2,=function+1
bx	r2
.pool

.org	function

ldr	r2,=branch+9
push	{r2}

mov	r2,0xe5
ldrb	r2,[r4,r2]
mov	r3,0x80
and	r2,r3
beq	end

mov	r2,0xfa
lsl	r2,r2,8
mov	r3,0x30
strh	r2,[r4,r3]

end:
lsr 	r1,r1,0x01
lsl 	r1,r1,0x02
add 	r1,r1, r0
ldr 	r1,[r1,0x00]
pop	{pc}

.pool
.close
