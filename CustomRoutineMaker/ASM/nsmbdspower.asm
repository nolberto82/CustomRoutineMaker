.nds
.create "out.bin", 0x00000000


.org	0x0211f878
bl	0x02000000

//ecode:
//.dw	 0xe2000000
//evalue:
//.dw	 0xe1d01adc

.org	0x02000000



push 	{r0-r5}
mov 	r3,#5
ldrb 	r2,[r0, 0xab]
ldrb 	r1,[r0, 0xa1]
ldrb 	r5,[r0, 0xa0]
ands 	r4,r1,2
beq 	end

ands	r4,r5,0x40
beq	next
adds 	r2,r2,1
cmp 	r2,r3
movgt 	r2,1
b	store

next:
ands	r4,r5,0x80
beq 	end
subs 	r2,r2,1
cmp 	r2,1
movlt 	r2,5
store:
strb 	r2, [r0, 0xab]
end:
pop 	{r0-r5}
ldrsb 	r1, [r0, 0xac]
bx 	lr

.close
