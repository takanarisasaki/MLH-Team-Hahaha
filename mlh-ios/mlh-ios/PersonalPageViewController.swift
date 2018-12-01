//
//  PersonalPageViewController.swift
//  mlh-ios
//
//  Created by Chen He on 2018-12-01.
//  Copyright © 2018 Chen He. All rights reserved.
//

import UIKit

class PersonalPageViewController: UIViewController {
    @IBOutlet var name: UITextField!
    @IBOutlet var phone: UITextField!
    @IBOutlet var email: UITextField!
    @IBOutlet var availability: UITextField!
    
    @IBOutlet var coursestack: UIStackView!
    override func viewDidLoad() {
        super.viewDidLoad()
        HTTP.post(addr: "users/getfullinfo", data:
            [
                "username":global.userdata["username"],
                "password":global.userdata["password"]
            ], f: updateinfo)
        var btn = UIButton()
        coursestack.addSubview(btn)
    }
    
    @IBAction func update(_ sender: Any) {
        HTTP.post(addr: "users/updateinfo", data:
            [
                "username":global.userdata["username"],
                "password":global.userdata["password"],
                "email":email.text!,
                "phone":phone.text!,
                "name":name.text!,
                "availability":availability.text!
            ], f: updateinfo)
    }
    
    func updateinfo(data:[String:Any])->Void{
        global.userdata["name"] = data["name"] as? String
        global.userdata["phone"] = data["phone"] as? String
        global.userdata["email"] = data["email"] as? String
        global.userdata["availability"] = data["availability"] as? String
        DispatchQueue.main.async {
            self.refresh()
        }
        
    }
    
    func refresh(){
        if global.userdata["name"] != nil {
            name.text = global.userdata["name"]!
        }
        if global.userdata["phone"] != nil {
            phone.text = global.userdata["phone"]!
        }
        if global.userdata["email"] != nil {
            email.text = global.userdata["email"]
        }
        if global.userdata["availability"] != nil {
            availability.text = global.userdata["availability"]
        }
        
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
