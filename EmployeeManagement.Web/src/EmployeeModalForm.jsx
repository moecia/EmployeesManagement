import { Modal, Form, Input, DatePicker, Transfer } from "antd";
import React, { useEffect, useState } from "react";
import moment from "moment";
import { EMPLOYEES_URL } from "./config";

export const EmployeeModalForm = ({ employeeData, tasksData, isEdit, visible, onCancel }) => {
  const [id, setId] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [hiredDate, setHiredDate] = useState("");
  const [employeeTasks, setEmployeeTasks] = useState(null);
  const [allTasks, setTasks] = useState(null);
  const [selectedKeys, setSelectedKeys] = useState([]);

  const dateFormat = "YYYY/MM/DD";
  const [form] = Form.useForm();

  useEffect(() => {
    if (visible) {
      form.resetFields();
      let t = [];
      let et = [];
      for(let i = 0; i < tasksData.length; ++i) {
        let task = tasksData[i];
        t.push({
          key: task.id,
          title: task.taskName,
          description: `Start time: ${task.startTime} Deadline: ${task.deadline}`,
        });
      }
      setTasks(t);
      if(isEdit) {
        setId(employeeData?.Id)
        setFirstName(employeeData?.firstName);
        setLastName(employeeData?.lastName);
        setHiredDate(employeeData?.hiredDate);
        for(let i = 0; i < employeeData?.task?.length; ++i) {
          let task = employeeData?.task[i];
          et.push(task.id);
        }    
      }
      setEmployeeTasks(et);
    }
  }, [visible, employeeData, form, isEdit, tasksData]);

  const onOk = () => {
    form.submit();
    console.log(`${firstName} ${lastName} ${hiredDate}`)
    if(firstName && lastName && hiredDate) {
      let et = [];
      for(let i in employeeTasks) {
        et.push(tasksData[i]);
      }
      if(isEdit) {
        let e = {
          Id: id,
          firstName: firstName,
          lastName: lastName,
          hiredDate: hiredDate,
          tasks: et
        };
        console.log(e);
        fetch(EMPLOYEES_URL, {
          method: "PUT",
          mode: "cors",
          body: JSON.stringify(e),
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
      } else {
        let e = {
          firstName: firstName,
          lastName: lastName,
          hiredDate: hiredDate,
          tasks: et
        };
        console.log(e);
        fetch(EMPLOYEES_URL, {
          method: "POST",
          mode: "cors",
          body: JSON.stringify(e),
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
          error.json().then((body) => {
            console.log(body);
          });
        });
      }
      onCancel();
      setTimeout(() => { window.location.reload(false) }, 2000);
    } else {
      alert("Please complete the form.");
    }
  }

  const onSelectChange = (sourceSelectedKeys, targetSelectedKeys) => {
    setSelectedKeys([...sourceSelectedKeys, ...targetSelectedKeys]);
  };

  function onFirstNameChange(firstName) {
    setFirstName(firstName);
  }

  function onLastNameChange(lastName) {
    setLastName(lastName);
  }

  function OnDateChange(date, dateString) {
    console.log(dateString);
    setHiredDate(dateString);
  }

  function onTasksChange(nextTargetKeys) {
    setEmployeeTasks(nextTargetKeys);
  }

  return (
    <Modal title={isEdit ? "Edit Employee" : "New Employee"} visible={visible} onOk={onOk} onCancel={onCancel}>
      <Form form={form} layout="vertical" name="employeeForm">
        <Form.Item
          name="First Name"
          label="First Name"
          initialValue={(isEdit && employeeData?.firstName) ? employeeData.firstName : ""}
          rules={[
            {
              required: firstName === "",
              message: 'Please input your first name!',
            },
          ]}
        >
          <Input onChange={(x) => {onFirstNameChange(x.target.value)}}/>
        </Form.Item>
        <Form.Item
          name="Last Name"
          label="Last Name"
          initialValue={(isEdit && employeeData?.lastName) ? employeeData.lastName : ""}
          rules={[
            {
              required: lastName === "",
              message: 'Please input your last name!',
            },
          ]}
        >
          <Input onChange={(x) => {onLastNameChange(x.target.value)}}/>
        </Form.Item>
        <Form.Item
          name="Hired Date"
          label="Hired Date"
          initialValue={(isEdit && employeeData?.hiredDate) ? moment(employeeData?.hiredDate, dateFormat) : ""}
          rules={[
            {
              required: !hiredDate,
              message: 'Please input an valid date!',
            },
          ]}
        >
          <DatePicker format={dateFormat} onChange={OnDateChange}/>
        </Form.Item>
        <Form.Item
          name="Tasks"
          label="Tasks"
        >
          <Transfer
            dataSource={allTasks == null ? [] : allTasks}
            titles={["Unassigned Tasked", "Assigned Tasks"]}
            targetKeys={employeeTasks == null ? [] : employeeTasks}
            selectedKeys={selectedKeys}
            onChange={onTasksChange}
            onSelectChange={onSelectChange}
            render={item => item.title}
          />
        </Form.Item>
      </Form>
    </Modal>
  );
};