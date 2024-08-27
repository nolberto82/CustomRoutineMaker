.nds
.create "out.bin", 0x00000000


.org	0x001f63e0
bl	0x005694c0


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x005694c0


add             r1, r4, #0x1000
add             r1, r1, #0xe0
ldr             r7, [r1,#0x8]
tst		r7,0x4
beq		end
ldr             r1, [r1,#0xc]
tst             r1, #1
ldrne           r1, =0x40500000
strne           r1, [r0,#0x10]
end:
mov             r1, r0

bx lr

.pool
.close
