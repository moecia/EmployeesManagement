import { List, Avatar, Modal, Button, PageHeader } from "antd";
import { useEffect, useState } from "react";
import styles from "./EmployeesManagement.css"
import { EmployeeModalForm } from "./EmployeeModalForm";
import { EMPLOYEES_URL, TASKS_URL } from "./config";

export function EmployeeManagement() {
  const [isEmployeesInit, setIsEmployeesInit] = useState(false)
  const [isTasksInit, setTaskInit] = useState(false)
  const [employees, setEmployees] = useState(null);
  const [tasks, setTasks] = useState(null);
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const [isEditModal, setIsEditModal] = useState(true);
  const [isEmployeeFormVisible, setIsEmployeeFormVisible] = useState(false);
  const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);

  useEffect(() => {
    getTasks();
    getEmployees();
  }, [employees, tasks, isEmployeesInit, isTasksInit]);

  function getEmployees() {
    if (!isEmployeesInit) {
      fetch(EMPLOYEES_URL, {
        method: "GET",
        mode: "cors"
      }).then(resp => {
        if (!resp.ok) {
          throw resp;
        }
        return resp.json();
      }).then(resp => {
        let e = [];
        for (let i = 0; i < resp.length; ++i) {
          let r = resp[i];
          e.push({
            Id: r.id,
            firstName: r.firstName,
            lastName: r.lastName,
            hiredDate: r.hiredDate,
            task: r.tasks,
          });
        }
        setEmployees(e);
        setIsEmployeesInit(true);
      }).catch(error => {
        console.log(error);
      });
    }
  }

  function getTasks() {
    if (!isTasksInit) {
      fetch(TASKS_URL, {
        method: "GET",
        mode: "cors"
      }).then(resp => {
        if (!resp.ok) {
          throw resp;
        }
        return resp.json();
      }).then(resp => {
        setTasks(resp);
        setTaskInit(true);
      }).catch(error => {
        console.log(error);
      });
    }
  }

  function NewEmployee() {
    setSelectedEmployee(null);
    setIsEmployeeFormVisible(true);
    setIsEditModal(false);
  }

  function editEmployee(employee) {
    setSelectedEmployee(employee);
    setIsEmployeeFormVisible(true);
    setIsEditModal(true);
  }

  function closeEmployeeForm() {
    setSelectedEmployee(null);
    setIsEmployeeFormVisible(false)
  }

  function deleteEmployee(employee) {
    setSelectedEmployee(employee);
    setIsDeleteModalVisible(true);
  }

  function confirmDelete() {
    fetch(EMPLOYEES_URL, {
      method: "DELETE",
      mode: "cors",
      body: JSON.stringify(selectedEmployee),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(resp => {
      if (!resp.ok) {
        throw resp;
      }
      return resp.json();
    }).catch(error => {
      console.log(error);
    });
    setIsDeleteModalVisible(false);
    setIsEmployeesInit(false);
    getEmployees();
  }

  function cancelDelete() {
    setSelectedEmployee(null);
    setIsDeleteModalVisible(false);
  }

  return (
    <>
      <PageHeader
        className="site-page-header-responsive"
        title="Employees Portal"
        extra={[
          <Button type="primary" onClick={NewEmployee}>
            New Employee
          </Button>
        ]}
      />
      <div className="container">
        <Modal title="Confirm Delete" visible={isDeleteModalVisible} onOk={confirmDelete} onCancel={cancelDelete}>
          <p>Are you sure to delete this employee infomation?</p>
        </Modal>
        <EmployeeModalForm employeeData={selectedEmployee} tasksData={tasks} isEdit={isEditModal} visible={isEmployeeFormVisible} onCancel={closeEmployeeForm} />
        <List
          itemLayout="horizontal"
          dataSource={employees !== null ? employees : []}
          renderItem={item => (
            <List.Item actions={[<a onClick={() => editEmployee(item)}>Edit</a>, <a onClick={() => deleteEmployee(item)}>Delete</a>]}>
              <List.Item.Meta
                avatar={<Avatar src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />}
                title={<p>{item?.firstName} {item?.lastName}</p>}
                description={<p>Employee From {item?.hiredDate}</p>}
                actions={[<a onClick={(item) => editEmployee(item)}>Edit</a>, <a key="list-loadmore-more">Delete</a>]}
              />
            </List.Item>
          )}
        />
      </div>
    </>
  )
}