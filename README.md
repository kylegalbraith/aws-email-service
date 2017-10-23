# aws-email-service
This project can be used to deliver email messages via one by one or via a SQS queue.
There is multiple flows that can be used to deliver email messages, each described in more detail below.

### Email messages written to S3 -> Send commands delivered to SQS -> Processed by Lambda
This is the initial design for aws-email-service. Messages can be written to S3 that follow the
`StoredEmailMessage` class. A `StoredEmailCommand` message is then put onto the SQS queue. The `SendEmail.Lambda`
can then be configured to watch the SQS Queue for new messages to deliver emails via SES.
