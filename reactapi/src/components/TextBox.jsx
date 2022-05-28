import '../styles/TextBox.css'

export default function TextBox(props) {
    return (
        <div className="textbox">
            <input type={props.obj.type}
                placeholder={props.obj.pholder}
                value={props.obj.value}
                onChange={props.obj.handleChangeProp}
                name={props.obj.name}
                autoComplete="off"
            />
        </div>
    );
}