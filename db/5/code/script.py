#!./venv/bin/python3

from cassandra.cluster import Cluster
from cassandra.auth import PlainTextAuthProvider
from concurrent.futures import ThreadPoolExecutor
from faker import Faker
from time import sleep

fake = Faker()

contact_points = ['0.0.0.0']
cassandra_port = 9100
keyspace = 'learn_cassandra'
table_name = 'users_by_country'

cluster = Cluster(contact_points=contact_points)
session = cluster.connect(keyspace)


def insert_data(session):
    insert_query = f"INSERT INTO {table_name}"
    insert_query += " (country, user_email, first_name, last_name, age)"
    insert_query += " VALUES"
    country = fake.country()
    country = country.replace("'", "_")
    insert_query += f" (\'{country}\', \'{fake.email()}\'"
    insert_query += f", \'{fake.first_name()}\', \'{fake.last_name()}\'"
    insert_query += f", {fake.random_int(min=18, max=90)});"
    session.execute(insert_query)

def single_threaded_insert(session, num_rows):
    for _ in range(num_rows):
        insert_data(session)

def multi_threaded_insert(session, num_rows, num_threads):
    with ThreadPoolExecutor(max_workers=num_threads) as executor:
        for _ in range(num_rows):
            executor.submit(insert_data, session)

def read_data(session, query):
    result = session.execute(query)

def single_threaded_read(session, query):
    return session.execute(query)

def multi_threaded_read(session, queries, num_threads):
    with ThreadPoolExecutor(max_workers=num_threads) as executor:
        for query in queries:
            result = executor.submit(query)

if __name__ == "__main__":
    num_rows = 100
    num_threads = 4

    print("write")
    single_threaded_insert(session, num_rows)
    print("sleep...")
    sleep(3)
    print("write")
    multi_threaded_insert(session, num_rows, num_threads)
    print("done")

    '''
    query = f"SELECT * FROM {keyspace}.{table_name};"
    try:
        result = session.execute(query)
        for row in result:
            print(row)
    except Exception as e:
        print(f"Error: {e}")
    '''

    sleep(10)

    queries = [
        "SELECT * FROM learn_cassandra.users_by_country;",
        "SELECT * FROM learn_cassandra.users_by_country WHERE age=30 ALLOW FILTERING;",
        "SELECT * FROM learn_cassandra.users_by_country WHERE country='USA';"
    ]
    thread_counts = [10, 25, 50, 100]

    for query in queries:
        print(single_threaded_read(session, query))

    for thread_count in thread_counts:
        print(f"read with {thread_count} thread")
        multi_threaded_read(session, queries, thread_count)

    session.shutdown()
    cluster.shutdown()

