.gba
.create "out.bin", 0x00000000
.definelabel branch, 0x800bcc0
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

add r1,0x10
ldr	r2,=0x02000160
ldrb	r2,[r2]
mov	r3,1
and	r2,r3
ldr 	r2,[r1,0x0c]
beq	end

cmp	r2,0
beq	end

sub	r3,5
lsl	r3,8
str	r3,[r1,0x0c]

end:
ldr 	r2,[r1,0x0c]
ldr 	r3,[r1,0x08]
cmp 	r2,r3

pop	{pc}

.pool
.close
