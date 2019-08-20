Implementing AccountBalance Exercice Using DDD / EventSourcing / CQRS Without a FrameWork 

The use cases for the Account are:

1.	An account can be created with an ID and a name of the account holder

2.	An overdraft limit can be set per account.

3.	A daily wire transfer limit can be set per account.

4.	Cheques can be deposited into an account. When a cheque is deposited, the funds are available on the next business day (defined as Monday to Friday, 9am-5pm).

5.	Cash can be deposited into an account. When cash is deposited, the funds are available immediately.

6.	Cash can be withdrawn from an account. Funds are removed immediately.

7.	A wire transfer can cause funds to be withdrawn from an account. Funds are removed immediately. If a wire transfer is attempted which is higher than the daily wire limit, the account should be placed into a blocked state.

8.	If a withdrawal would cause the account balance to be negative by more than overdraft limit for the account, the withdrawal should not succeed, and the account should be placed into a blocked state.

9.	A deposit made to an account in the blocked state should unblock the account when the funds become available for use.
