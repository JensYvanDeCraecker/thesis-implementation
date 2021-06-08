import { StateInfo } from '@/interfaces';
import { FC, useState } from 'react';
import { Button } from './Button';
import { Select } from './Select';

export interface UpdateStateProps {
    states?: StateInfo[];
    onUpdate?: (stateId: number) => void;
}

export const UpdateState: FC<UpdateStateProps> = props => {
    const { states, onUpdate } = props;

    const [stateId, setStateId] = useState<number>();

    const handleClick = () => {
        if (!stateId) return;
        if (onUpdate) onUpdate(stateId);
        setStateId(undefined);
    };

    return (
        <div className="c-update-state">
            <Select value={stateId?.toString()} items={states?.map(state => ({ name: state.name, value: state.id.toString() }))} onChange={newValue => setStateId(parseInt(newValue))} disabled={!states} />
            <Button text="Update parcel" onClick={handleClick} disabled={!stateId} />
        </div>
    );
};
