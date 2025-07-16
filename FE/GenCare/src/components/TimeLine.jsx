import React from 'react';

const TestProgressTimeline = ({ steps, processes }) => {
    const getStatus = (stepId) => {
        const process = processes.find(p => p.stepId === stepId);
        return process?.status?.statusName || "Unknown";
    };

    const isStepCompleted = (stepId) => {
        const process = processes.find(p => p.stepId === stepId);
        return process?.status?.statusName === "Hoàn thành"; // tùy chỉnh theo tên status
    };

    return (
        <div className="d-flex justify-content-between align-items-center mb-4" style={{ position: 'relative' }}>
        <div className="progress-line" style={{
            position: 'absolute',
            height: '4px',
            width: '100%',
            backgroundColor: '#ddd',
            top: '20px',
            zIndex: 0
        }}></div>

        {steps.map((step, index) => {
            const completed = isStepCompleted(step.stepId);
            return (
            <div key={step.stepId} className="text-center position-relative" style={{ zIndex: 1, width: '12%' }}>
                <div
                className={`rounded-circle mx-auto mb-1 ${completed ? 'bg-success' : 'bg-light'}`}
                style={{
                    width: '40px',
                    height: '40px',
                    border: '3px solid green',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    color: completed ? 'white' : 'green'
                }}
                >
                {index + 1}
                </div>
                <div style={{ fontSize: '12px', fontWeight: '500' }}>{step.stepName}</div>
                <div style={{ fontSize: '10px', color: 'gray' }}>{getStatus(step.stepId)}</div>
            </div>
            );
        })}
        </div>
    );
};

export default TestProgressTimeline;
