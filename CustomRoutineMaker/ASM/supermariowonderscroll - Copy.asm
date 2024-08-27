//.swi

.org	0x0165aaac-0x4000
bl	main


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x02012c20
main:


sub	sp,sp,0x10
stp	x8,x9,[sp]
ldr     s0, [x9]

.word 	0xd000b0e9
ldr	x9,[x9,0xa98]
ldr	x9,[x9,0x20]
ldr	x9,[x9]
ldrh    w9,[x9,0x12]

ands	w8,w9,0x400
beq	right

fmov	s0,-7.5

right:

ands	w8,w9,0x800
beq	end

fmov	s0,7.5

end:
ldp	x8,x9,[sp]
add	sp,sp,0x10
ret

