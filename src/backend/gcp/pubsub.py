from google.cloud import pubsub_v1
import time
import json

project_id = "tonal-mote-246514"
subscription_name = "questions-sub" 

subscriber = pubsub_v1.SubscriberClient()
# The `subscription_path` method creates a fully qualified identifier
# in the form `projects/{project_id}/subscriptions/{subscription_name}`
subscription_path = subscriber.subscription_path(project_id, subscription_name)

def callback(message):
    data = json.loads(message.data.decode('utf-8'))
    print("Recieved message: {}".format(data))
    message.ack()
    return data


def pullQuestion():
    response = subscriber.pull(subscription_path, max_messages = 1)
    print('Listening for messages on {}'.format(subscription_path))

    for msg in response.received_messages:
        data = json.loads(msg.message.data.decode('utf-8'))

    # if no messgae is recieved, then to handle failure while acknowledging
    try:
        ack_ids = [msg.ack_id for msg in response.received_messages]
        subscriber.acknowledge(subscription_path, ack_ids)
    except Exception as ex:
        subscription.close()

    return data


data = pullQuestion()
print("Recieved message: {}".format(data))